namespace Netolex.Services.Document_Settings
{
    public class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile formFile, string folderName)
        {
            if (formFile == null || formFile.Length == 0)
                return null;

            //1-get folder Name okey
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            //2-check if the folder is already exist 
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //3-get file name 
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";

            //4-get the file Path 
            string filePath = Path.Combine(folderPath, fileName);

            //5-save the file path
            using var fileStream = new FileStream(filePath, FileMode.Create);

           await  formFile.CopyToAsync(fileStream);
            return fileName;
        }

        public static bool DeleteFile(string folderName, string fileName)
        {
            try
            {
                // 1. Get the full folder path
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

                // 2. Get the full file path
                string filePath = Path.Combine(folderPath, fileName);

                // 3. Check if the file exists
                if (File.Exists(filePath))
                {
                    // 4. Delete the file
                    File.Delete(filePath);
                    return true; // Deletion successful
                }
                else
                {
                    return false; // File not found
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }
    }
}
