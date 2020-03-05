using NHibernate;
using System;

namespace IntelligentHabitacion.Api.Repository.WorkUnit
{
    public class WorkUnitNHibernate
    {
        [ThreadStatic]
        private static WorkUnitNHibernate _workUnitNHibernateActive;

        private readonly ITransaction _transacao;

        public WorkUnitNHibernate(ISessionFactory sessionFactory)
        {
            Session = sessionFactory.OpenSession();

            _transacao = Session.BeginTransaction();
        }

        public static WorkUnitNHibernate WorkUnitNHibernateActive
        {
            get { return _workUnitNHibernateActive; }
            set { _workUnitNHibernateActive = value; }
        }

        public ISession Session { get; set; }

        public void Commit()
        {
            _transacao.Commit();
        }

        public void Rollback()
        {
            _transacao.Rollback();
            Session.Clear();
        }

        public void Close()
        {
            Session.Close();
            WorkUnitNHibernateActive = null;
        }
    }
}
