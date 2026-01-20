using System.Data;

public class Diplomarbeit
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public Diplomarbeit(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static async Task<List<Diplomarbeit>> LoadDiplomarbeitenAsync()
    {
        return await Task.Run(() =>
        {
            DbWrapperMySqlV2 wrapper = DbWrapperMySqlV2.Wrapper;

            try
            {
                List<Diplomarbeit> diplomarbeiten = new();

                string sql =
                    "SELECT id, diplomarbeit FROM diplomarbeiten";

                DataTable dt = wrapper.RunQuery(sql);

                foreach (DataRow row in dt.Rows)
                {
                    Diplomarbeit da = new(
                        Convert.ToInt32(row[0]),
                        row[1]?.ToString() ?? string.Empty
                    );

                    diplomarbeiten.Add(da);
                }

                return diplomarbeiten;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Diplomarbeit>(); // besser als null
            }
        });
    }


}

