namespace ClearWork.Application.TimeEntries.Dtos;

public class AudioNoteDto
{
    public byte[] AudioData { get; set; }
    public string FileFormat { get; set; } = null!;
    public float DurationSeconds { get; set; }
    public DateTimeOffset RecordedAt { get; set; }
}