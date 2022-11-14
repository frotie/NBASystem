using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Models
{
    class DbPagination<TEntity> 
        where TEntity : class
    {
        public int TotalRecords { get; private set; }
        public int PagesCount { get; private set; }
        public uint RowsInPage { get; private set; }

        private Func<TEntity, bool> _predicate;
        private DbContext _db;
        private DbSet<TEntity> _dbSet;
        public DbPagination(DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
            _predicate = (o => true);
            RowsInPage = 10;

            updateData();
        }

        public void SetPredicate(Func<TEntity, bool> check)
        {
            _predicate = check;
            updateData();
        }

        public void SetPagesPerRow(uint rows)
        {
            RowsInPage = rows;
            updateData();
        }

        public List<TEntity> GetPage(int page)
        {
            return _dbSet.AsNoTracking().Where(_predicate).Skip(page - 1).Take((int)RowsInPage).ToList();
        }

        private void updateData()
        {
            TotalRecords = _dbSet.Where(_predicate).Count();
            PagesCount = (int)Math.Ceiling((double)TotalRecords / RowsInPage);
        }
    }
}
