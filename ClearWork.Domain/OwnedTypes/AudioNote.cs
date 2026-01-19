namespace ClearWork.Domain.OwnedTypes;

/// <summary>
/// Reprezentuje nagranie głosowe (notatkę audio) dołączone do wpisu czasu.
/// </summary>
public class AudioNote
{
    /// <summary>
    /// Dane binarne nagrania audio.
    /// </summary>
    public required byte[] AudioData { get; set; }
    
    /// <summary>
    /// Format pliku audio.
    /// </summary>
    public string FileFormat { get; set; } = null!;
    
    /// <summary>
    /// Czas trwania nagrania w sekundach.
    /// </summary>
    public required float DurationSeconds { get; set; }
    
    /// <summary>
    /// Data i godzina nagrania notatki audio.
    /// </summary>
    public DateTimeOffset RecordedAt { get; set; }
}