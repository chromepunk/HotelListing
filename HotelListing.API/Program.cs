using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors
builder.Services.AddCors(options =>
{
    //"allowall is the name of the policy
    options.AddPolicy("AllowAll", b =>
    {
        //because its a builder we can daisy chain
        b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

//configure serilog
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//use cors: AllowAll
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
