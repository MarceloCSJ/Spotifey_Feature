using ArtistConnect.Classes;
 
Console.Clear();
MenuPrincipal menuPrincipal = new(new MenuArtista(), new MenuMusica(), new MenuGenero());
await menuPrincipal.ExecutarAsync();