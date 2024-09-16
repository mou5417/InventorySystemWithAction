using Database.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InventorySystem.db");
var conn = new SqliteConnection($"Data Source={dbPath}");
var logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "log.txt");
try
{
    builder.Services.AddDbContext<InventoryDbContext>(option => option.UseSqlite(conn));
    builder.Services.AddIdentityCore<User>().AddRoles<Role>().AddEntityFrameworkStores<InventoryDbContext>().AddUserStore<UserStore<User, Role, InventoryDbContext, Guid>>().AddRoleStore<RoleStore<Role, InventoryDbContext, Guid>>();
}
catch (Exception ex)
{

    File.AppendAllText(logFilePath, $"{DateTime.Now}: {ex.Message}\n");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Signingkey"]))
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    dbContext.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

#region Get  data

// Get List Data from database
app.MapGet("api/Get/Items", async (InventoryDbContext db) =>
{
    var items = await db.Items.Include(i => i.ItemLocation).Include(i => i.CreateUser).ToListAsync();
    var itemResponse = items.Select(i => DtoFactory.ItemToItemForUI(i)).ToList();
   
    return Results.Ok(itemResponse);

});
app.MapGet("api/Get/ItemLocations", async (InventoryDbContext db) =>
{
    var itemlocations = await db.ItemLocations.ToListAsync();
    return Results.Ok(itemlocations);

});

app.MapGet("api/Get/Users", async (InventoryDbContext db) =>
{
    var users = await db.Users.ToListAsync();
    return Results.Ok(users);

}).RequireAuthorization();

//Get single data from database
// Dedicated route for accessing users by ID
app.MapGet("api/GetUser/{id}", async ([FromRoute] Guid id, InventoryDbContext db) =>
{
    if (id == Guid.Empty)
    {
        return Results.Problem("id can't be empty");
    }

    var userResponse = await GetUserById(id, db);
    return userResponse;
}).RequireAuthorization();

// Route for accessing other entities (Items, ItemLocations) by tableName and ID
app.MapGet("api/Get/{tableName}/{id}", async ([FromRoute] string tableName, [FromRoute] Guid id, InventoryDbContext db) =>
{
    if (id == Guid.Empty)
    {
        return Results.Problem("id can't be empty");
    }

    var response = tableName.ToLower() switch
    {
        "items" => await GetItemById(id, db),
        "itemlocations" => await GetItemLocationById(id, db),
        _ => Results.NotFound()
    };

    return response;
});

// Helper method for retrieving an item by ID
async Task<IResult> GetItemById(Guid id, InventoryDbContext db)
{
    var itemResponse = await db.Items
        .Include(i => i.ItemLocation)
        .Include(i => i.CreateUser)
        .FirstOrDefaultAsync(i => i.ItemId == id);

    return itemResponse != null ? Results.Ok(itemResponse) : Results.NotFound();
}

// Helper method for retrieving an item location by ID
async Task<IResult> GetItemLocationById(Guid id, InventoryDbContext db)
{
    var locationsResponse = await db.ItemLocations.FirstOrDefaultAsync(l => l.LocationId == id);
    return locationsResponse != null ? Results.Ok(locationsResponse) : Results.NotFound();
}

// Helper method for retrieving a user by ID
async Task<IResult> GetUserById(Guid id, InventoryDbContext db)
{
    var userResponse = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
    return userResponse != null ? Results.Ok(userResponse) : Results.NotFound();
}

#endregion



#region Post data

app.MapPost("/api/Post/items", async ([FromBody] Item item, InventoryDbContext db) =>
{
    if (checkNull(item))
    {
        try
        {
            await db.SaveChangesAsync();
            return Results.Ok(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem("there is problem");
        }
    }

    return Results.Problem("object can't be empty");
});

app.MapPost("/api/Post/itemlocations", async ([FromBody]ItemLocation item, InventoryDbContext db) =>
{
    if (checkNull(item))
    {
        try
        {
            await db.ItemLocations.AddAsync(item);
            await db.SaveChangesAsync();
            return Results.Ok("location data added");
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    return Results.Problem("object can't be empty");
});

app.MapPost("/api/Post/users", async ([FromBody] User user, InventoryDbContext db) =>
{
    if (checkNull(user))
    {
        try
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return Results.Ok(" user data added");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem(ex.Message);
        }

    }
    return Results.Problem("object can't be empty");
});

#endregion

#region Updata data

app.MapPut("/api/Put/items/{id}", async ( [FromRoute] Guid id, Item source, [FromServices] InventoryDbContext db) =>
{
    if (checkNull(id) && checkNull(source))
    {
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            var itemToUpdate = await db.Items.FindAsync(id);
            if (itemToUpdate is null) return Results.NotFound();
            itemToUpdate.ItemId = source.ItemId;
            itemToUpdate.ItemName = source.ItemName;
            itemToUpdate.PurchaseId = source.PurchaseId;
            itemToUpdate.ItemLocationId = source.ItemLocationId;
            itemToUpdate.ItemLocation = source.ItemLocation;
            itemToUpdate.Description = source.Description;
            itemToUpdate.CreateUserId = source.CreateUserId;
            itemToUpdate.Status = source.Status;
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return Results.Ok(itemToUpdate);
        }
    }

    return Results.NoContent();
});

app.MapPut("/api/Put/ItemLocations/{id}", async ( [FromRoute] Guid id, ItemLocation source, [FromServices] InventoryDbContext db) =>
{
    if (checkNull(id)&&checkNull(source))
    {
        using var transaction = await db.Database.BeginTransactionAsync();
        var itemToUpdate = await db.ItemLocations.FindAsync(id);

        if (itemToUpdate is null) return Results.NotFound();
        itemToUpdate.LocationId = source.LocationId;
        itemToUpdate.LocationName = source.LocationName;
        itemToUpdate.LocationDetail = source.LocationDetail;
        itemToUpdate.Items = source.Items;
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
        return Results.Ok(itemToUpdate);
    }

    return Results.NoContent();
});
app.MapPut("/api/Put/Users/{id}", async ([FromRoute] Guid id, User source, [FromServices] InventoryDbContext db) =>
{
    //ToDo add check to prevent self role change
    if (checkNull(id) && checkNull(source))
    {
        using var transaction = await db.Database.BeginTransactionAsync();
        var itemToUpdate = await db.Users.FindAsync(id);

        if (!checkNull(itemToUpdate)) return Results.NotFound();
        itemToUpdate.UserName = source.UserName;
        itemToUpdate.RoleLevel = source.RoleLevel;
        itemToUpdate.Department = source.Department;
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
        return Results.Ok(itemToUpdate);
    }

    return Results.NoContent();
});

#endregion
app.MapDelete("/api/{tableName}/{id}", async ([FromRoute] string tableName, [FromRoute] Guid id, [FromServices] InventoryDbContext db) =>
{
    switch (tableName.ToLower())
    {
        case "items":

            var tempItem = await db.Items.FindAsync(id);
            if (tempItem != null)
            {
                db.Items.Remove(tempItem);
                await db.SaveChangesAsync();
                return Results.Ok($"{tempItem} is deleted");
            }
            else { return Results.NoContent(); }

        case "itemlocations":
            ItemLocation tempLocation = await db.ItemLocations.FindAsync(id);
            if (tempLocation != null)
            {
                db.ItemLocations.Remove(tempLocation);
                await db.SaveChangesAsync();
                return Results.Ok($"{tempLocation} is deleted");

            }
            return Results.NotFound();

        case "users":

            var tempUser = await db.Users.FindAsync(id);
            if (tempUser != null)
            {
                db.Users.Remove(tempUser);
                await db.SaveChangesAsync();
                return Results.Ok($"{tempUser} is deleted");

            }
            return Results.NotFound();

        default: return Results.NoContent();

    }




});

app.MapPost("/api/login", async (LogindDto logindDto, UserManager<User> _userManager) => {
    var user =await _userManager.FindByNameAsync(logindDto.Username);
    if (user is null) return Results.Unauthorized();
    var isValidPassword = await _userManager.CheckPasswordAsync(user, logindDto.Password);
    if(!isValidPassword) return Results.Unauthorized();

    //Generate an access token

    var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Signingkey"]));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var roles= await _userManager.GetRolesAsync(user);
    var claims = await _userManager.GetClaimsAsync(user);
    var takonClaims=new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email,user.Email),
        new Claim(ClaimTypes.Role,user.RoleLevel.ToString()),
    }.Union(claims).Union(roles.Select(role=>new Claim(ClaimTypes.Role,role)));
    var securityToken = new JwtSecurityToken(
        issuer: builder.Configuration["JwtSettings:Issuer"],
        audience: builder.Configuration["JwtSettings:Audience"],
        claims: takonClaims,
        expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["JwtSettings:DurationMinutes"])),
        signingCredentials: credentials);
        
    var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
    var response = new AuthResponseDto
    {
        UserID = user.Id.ToString(),
        UserName= user.UserName,
        PersonName= user.PersonName,
        Token = accessToken
    };
    return Results.Ok(response);
});

app.Run();

static  bool checkNull<T>(T obj)
{
    if (obj != null)
    {
        return true;
    }
    return false;
}


internal class AuthResponseDto
{
    public string UserID { get; set; }
    public string Password { get; set; }

    public string PersonName { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
}
internal class LogindDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}


