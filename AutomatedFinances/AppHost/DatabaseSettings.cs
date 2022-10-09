using Microsoft.EntityFrameworkCore;

namespace AppHost;

public record DatabaseSettings(string ConnectionString, MySqlServerVersion ServerVersion);
