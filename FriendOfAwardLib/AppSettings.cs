using System.Data;

public class AppSettings
{
    public bool VotingActive { get; set; }
    public DateTime VotingEnd { get; set; }

    public static AppSettings Load()
    {
        DbWrapperMySqlV2 db = DbWrapperMySqlV2.Wrapper;
        DataTable dt = new();
        try
        {
            dt = db.RunQuery(
            "SELECT voting_active, voting_end FROM foa_app_settings LIMIT 1");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        

        DataRow row = dt.Rows[0];

        return new AppSettings
        {
            VotingActive = Convert.ToInt32(row["voting_active"]) == 1,
            VotingEnd = Convert.ToDateTime(row["voting_end"])
        };
    }

    public static void Update(DateTime endTime, bool active)
    {
        DbWrapperMySqlV2 db = DbWrapperMySqlV2.Wrapper;
        try
        {
            db.RunNonQuery($"""
            UPDATE foa_app_settings
            SET voting_end = '{endTime:yyyy-MM-dd HH:mm:ss}',
                voting_active = {(active ? 1 : 0)},
                updated_at = NOW()
        """);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
}
