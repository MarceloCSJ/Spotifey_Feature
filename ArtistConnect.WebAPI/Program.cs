var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var artistConnectGroup = app.MapGroup("/artist-connect");

artistConnectGroup.MapGet("/", () => {
    
});

//artistConnectGroup.MapPost("/", ([FromBody]) Artista artista) =>{
    
//});