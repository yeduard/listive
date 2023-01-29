namespace Logins.Database;

public interface IUnitOfWork
{
    Task<int> CommitAsync();

    void Dispose();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly LoginContext _context;

    public UnitOfWork(LoginContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}