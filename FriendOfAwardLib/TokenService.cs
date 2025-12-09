using System;
using System.Data;

public class TokenService
{
    private readonly DbWrapperMySqlV2 db = DbWrapperMySqlV2.Wrapper;

    // Token generieren (z.B. für QR-Code)
    public string GenerateToken()
    {
        return Guid.NewGuid().ToString("N"); // 32 Zeichen
    }

    // Token in DB speichern
    public void SaveToken(string token)
    {
        string sql =
            $"INSERT INTO user_tokens (token, created_at) VALUES ('{token}', NOW())";

        db.RunNonQuery(sql);
    }

    // Token prüfen (für User Login)
    public bool ValidateToken(string token)
    {
        string sql =
            $"SELECT id, is_used FROM user_tokens WHERE token = '{token}' LIMIT 1";

        DataTable dt = db.RunQuery(sql);

        if (dt.Rows.Count == 0)
            return false; // Token existiert nicht

        bool isUsed = Convert.ToBoolean(dt.Rows[0]["is_used"]);
        if (isUsed)
            return false; // Schon verbraucht

        return true;
    }

    // Token als “benutzt” markieren (optional)
    public void MarkTokenAsUsed(string token)
    {
        string sql =
            $"UPDATE user_tokens SET is_used = TRUE WHERE token = '{token}'";

        db.RunNonQuery(sql);
    }
}
