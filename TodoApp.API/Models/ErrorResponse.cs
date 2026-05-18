namespace TodoApp.API.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP status code (400, 404, 500, dll)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Judul singkat error (misal: "Not Found", "Bad Request")
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Penjelasan detail error yang terjadi
        /// </summary>
        public string Detail { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp kapan error terjadi (UTC)
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Khusus untuk validation error: daftar field yang bermasalah
        /// </summary>
        public IDictionary<string, string[]>? Errors { get; set; }
    }
}
