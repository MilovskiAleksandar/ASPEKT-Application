namespace CodeFix
{
    public class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Users\aleks\OneDrive\Desktop\testFolder";
            List<string> txtFiles = new List<string>();
            GetTxtFiles(directoryPath, txtFiles);

            foreach (var file in txtFiles)
            {
                AppendTextToFile(file, "ASPEKT");
            }
        }

        static void GetTxtFiles(string directoryPath, List<string> txtFiles)
        {
            string[] files = Directory.GetFiles(directoryPath, "*.txt");
            txtFiles.AddRange(files);

            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
            {
                GetTxtFiles(subdirectory, txtFiles); //Changed
            }
        }

        static void AppendTextToFile(string filePath, string textToAppend)
        {
            StreamWriter writer = null;
            writer = File.AppendText(filePath);
            writer.WriteLine(textToAppend);
            writer.Close(); //Added
        }


        //Console application that recursively searches for .txt files in a specified directory and its subdirectories, appends the text "ASPEKT" to each found file.
        //The first bug is in GetTxtFiles method in the foreach, the issue is that it calls GetTxtFiles with the directoryPath instead of subdirectory.
        //this causes the method to repeatedly search the same directoryPath leading to infinite recursion.
        //The second bug is in AppendTextToFile method, the issue is that we dont close the StreamWriter, if we dont close the StreamWriter we may encounter file
        //lock error because the file is still considered IN USE.

    }
}



