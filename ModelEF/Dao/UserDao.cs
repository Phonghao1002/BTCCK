using ModelEF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace ModelEF.Dao
{
    public class UserDao
    {
        PhamPhongHaoContext db = null;
        public UserDao()
        {
            db = new PhamPhongHaoContext();
        }
        public long  Insert(UserAccount entity)
        {
            db.UserAccounts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(UserAccount entity)
        {
            try
            {
                var User = db.UserAccounts.Find(entity.ID);
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    User.Password = entity.Password;
                }
                User.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public IEnumerable<UserAccount> ListAllPaging(int page,int pageSize)
        {
            return db.UserAccounts.OrderBy(x=>x.ID).ToPagedList(page, pageSize);
        }
        public UserAccount GetById(string userName)
        {
            return db.UserAccounts.SingleOrDefault(x => x.Username == userName);
        }
        public UserAccount ViewDetail(int id)
        {
            return db.UserAccounts.Find(id);
        }

        public int Login(string userName, string password)
        {
            var result = db.UserAccounts.SingleOrDefault(x => x.Username == userName);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == null)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var userAccount = db.UserAccounts.Find(id);
                db.UserAccounts.Remove(userAccount);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
