using System;
using System.Linq;

namespace DesignPatterns
{
    /// <summary>
    /// Treat individual objects and compositions of objects uniformly.
    /// </summary>
    class Composite : IDesignPattern
    {
        public void DisplayExample()
        {
            IUser userGroupA = new UserGroup(new[] { new User(1), new User(6) });
            IUser userGroupB = new UserGroup(new[] { userGroupA, new User(3) });
            Console.WriteLine(userGroupB.GetMoney());
        }

        #region Implementation
        interface IUser
        {
            int GetMoney();
        }

        class User : IUser
        {
            private int money;

            public User(int money)
            {
                this.money = money;
            }

            public int GetMoney()
            {
                return money;
            }
        }

        class UserGroup : IUser
        {
            private IUser[] users;

            public UserGroup(IUser[] users)
            {
                this.users = users;
            }

            public int GetMoney()
            {
                return users.Sum(user => user.GetMoney());
            }
        }
        #endregion
    }
}
