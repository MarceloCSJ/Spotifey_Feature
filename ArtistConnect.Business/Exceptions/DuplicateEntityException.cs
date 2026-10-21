namespace ArtistConnect.Business.Exceptions;

public sealed class DuplicateEntityException(string message) : Exception(message);
