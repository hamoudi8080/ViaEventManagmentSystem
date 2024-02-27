namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

public class Program
{
   public static void Main()
    {
        // Simulate file upload
        Result uploadResult = UploadFile("example.txt");

        // Process the uploaded file
        Result processResult = ProcessFile("example.txt");

        // Display the results
        Console.WriteLine("Upload Result:");
        DisplayResult(uploadResult);

        Console.WriteLine("\nProcessing Result:");
        DisplayResult(processResult);
    }

    static Result UploadFile(string fileName)
    {
        // Simulate file upload process
        // For demonstration purposes, let's assume the upload is successful
        Console.WriteLine($"Uploading {fileName}...");
        return Result.Success();
    }

    static Result ProcessFile(string fileName)
    {
        // Simulate file processing
        // For demonstration purposes, let's assume the processing fails
        Console.WriteLine($"Processing {fileName}...");
        List<Error> errors = new List<Error>
        {
            new Error(1, "Invalid file format"),
            new Error(2, "Processing timed out")
        };
        return Result.Failure(errors);
    }

    static void DisplayResult(Result result)
    {
        if (result.IsSuccess)
        {
            Console.WriteLine("Operation succeeded!");
        }
        else
        {
            Console.WriteLine("Operation failed with the following errors:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error {error.ErrorCode}: {error.ErrorMessage}");
            }
        }
    }
}