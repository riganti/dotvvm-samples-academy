#load "../20_first_task"
GetType("System.Int32").IsAllowed = true;
var add = GetMethod("System.Int32 SampleCourse.Test::Add(System.Int32,System.Int32)");
Validate(c =>
{
    var t = c.Instantiate(test);
    if (t.Add(1, 1) != 2)
    {
        c.Report("Your Add method doesn't add correctly.");
    }
});