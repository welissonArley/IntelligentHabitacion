using NHibernate;
using System;

namespace IntelligentHabitacion.Api.Repository.WorkUnit
{
    public class WorkUnitNHibernate
    {
        [ThreadStatic]
        private static WorkUnitNHibernate _workUnitNHibernateActive;

        private readonly ITransaction _transaction;

        public WorkUnitNHibernate(ISessionFactory sessionFactory)
        {
            Session = sessionFactory.OpenSession();
            Session.FlushMode = FlushMode.Manual;

            _transaction = Session.BeginTransaction();
        }

        public static WorkUnitNHibernate WorkUnitNHibernateActive
        {
            get { return _workUnitNHibernateActive; }
            set { _workUnitNHibernateActive = value; }
        }

        public ISession Session { get; set; }

        public void Commit()
        {
            if(_transaction.IsActive)
                _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Session.Clear();
        }

        public void Close()
        {
            Session.Close();
            WorkUnitNHibernateActive = null;
        }
    }
}
