using System.Data;

public class Diplomarbeit
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int PunkteSchulvoting { get; private set; }
    public int PunktePublikumsvoting { get; private set; }

    public Diplomarbeit(int id, string name, int punkteSchulvoting, int punktePublikumsvoting)
    {
        Id = id;
        Name = name;
        PunktePublikumsvoting = punkteSchulvoting;
        PunktePublikumsvoting = punktePublikumsvoting;
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
                    "SELECT id, diplomarbeit, punkteSchulvoting, punktePublikumsvoting FROM diplomarbeiten";

                DataTable dt = wrapper.RunQuery(sql);

                foreach (DataRow row in dt.Rows)
                {
                    Diplomarbeit da = new(
                        Convert.ToInt32(row[0]),
                        row[1]?.ToString() ?? string.Empty,
                        Convert.ToInt32(row[2]),
                        Convert.ToInt32(row[3])
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

