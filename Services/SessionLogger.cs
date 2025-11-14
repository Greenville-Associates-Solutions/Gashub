namespace Enterpriseservices
{
  public class SessionsLogger
    {
        public string? testsession;
        public static void SessionLog(string username, int hashid, DateTime sessionstart, DateTime sessionend, string moduleid, string description)
        {
            var logsessionstring = username + "," + hashid + "," + sessionstart + "," + sessionend;
            return;
        }

    }
}