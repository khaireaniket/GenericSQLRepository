# GenericRepository.SQL 
This is a .Net Standard library which uses UnitOfWork(UoW) and Repository pattern to handle both Sql Server and MySql as a backend.

**Steps to consume the library:**

- In appsettings.json or web.config file, add Sql Server and MySql database configuration in following format,

```json
"ConnectionStrings": 
{
  "SqlServerConnectionString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=localhost;Integrated Security=True;",
  "MySqlConnectionString": "Server=127.0.0.1;Database=localhost;Uid=admin;Pwd=admin;"
}
```

- In Startup.cs add the following code,

```csharp
services.AddSQLDependencies();
services.AddDbContext<GenericRepositoryDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnectionString"))); 
services.AddDbContext<GenericRepositoryDBContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySqlConnectionString")));
```

- [x] Use either "UseSqlServer" or "UseMySQL", depeding on which backend you wish to configure.
- [x] Here GenericRepositoryDBContext is a database context class, refer point 4.
- [x] This line will connect your application to either Sql Server or MySql database, depending on the configuration used.

- To start using the repository, firstly create a simple entity as follows,

```csharp
public class Person
{
    // Properties of entity
}
```

- Once entity classes are created, create a context class as follows,

```csharp
public class GenericRepositoryDBContext : DbContext
{
    public GenericRepositoryDBContext(DbContextOptions<GenericRepositoryDBContext> options) : base(options)
    {

    }

    public virtual DbSet<Person> Person { get; set; }
}
```

> Please note: All the repository methods are implemented as virtual, meaning you have an option of overriding repository methods as per needs.

- Following is the list of repository methods supported in the current version,

```csharp
Add(T entity);
AddAsync(T entity);
Update(T entity);
Remove(T entity);
GetById<TKey>(TKey id);
GetByIdAsync<TKey>(TKey id);
GetAll();
GetAll(int page, int pageCount);
GetAll(string[] includes);
GetAll(int page, int pageCount, string[] includes);
FindBy(Expression<Func<T, bool>> predicate);
FindBy(Expression<Func<T, bool>> predicate, string include);
FindBy(Expression<Func<T, bool>> predicate, string[] includes);
RawSql(string query, params object[] parameters);
Exists(Expression<Func<T, bool>> predicate);
```

> You can always refer to 'HowToConsume.GenericRepository.SQL' project to understand how to consume the library
