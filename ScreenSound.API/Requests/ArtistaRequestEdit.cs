namespace ScreenSound.API.Requests;

public record ArtistaRequestEdit(string nome, string bio, int id): ArtistaRequest(nome,bio);

