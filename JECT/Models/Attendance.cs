namespace JECT.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
