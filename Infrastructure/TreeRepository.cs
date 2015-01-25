using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Conventions;
using Infrastructure.Entities;
using Infrastructure.Entities.TreeTypeDaos;
using Infrastructure.Extensions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace Infrastructure
{
    public class TreeRepository : ITreeRepository
    {
        private readonly ISession _session;

        public TreeRepository()
        {
            _session = SessionManager.CurrentSession;
            _session.FlushMode = FlushMode.Always;
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }        

        public List<VirtualTreeDao> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false)
        {
            using (var transaction = _session.StartTransaction())
            {
                List<TreeParentDao> treeParentDaos;

                if (includeParent)
                {
                    if (includeDeleted)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level <= 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level <= 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .Where(tp => tp.TreeChild.State.Id.ToString() != ObjectStates.osDeleted.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();   
                    }                    
                }
                else
                {
                    if (includeDeleted)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level == 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level == 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .Where(tp => tp.TreeChild.State.Id.ToString() != ObjectStates.osDeleted.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    
                }

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeParentDao in treeParentDaos)
                {
                    var _treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent.Id.ToString() == treeParentDao.TreeChild.Id.ToString())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        Id = treeParentDao.TreeChild.Id,
                        Parent = treeParentDao.TreeChild.Parent,
                        Name = treeParentDao.TreeChild.Name,
                        ShortName = treeParentDao.TreeChild.ShortName,
                        Type = treeParentDao.TreeChild.Type,
                        State = treeParentDao.TreeChild.State,
                        CreateDateTime = treeParentDao.TreeChild.CreateDateTime,
                        HasChildren = _treeParentDaos.Count > 0
                    });
                }

                return virtualTreeDaos;
            }
        }

        public List<VirtualTreeDao> GetTreeParents(Guid? parent, Guid child, Guid treeParentType,
            bool includeChild = false, bool includeDeleted = false)
        {
            using (var transaction = _session.StartTransaction())
            {
                List<TreeParentDao> treeParentDaos;

                if (parent != null)
                {
                    var firstOrDefault = _session.Query<TreeParentDao>()
                        .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString()).FirstOrDefault(tp => tp.TreeChild.Id.ToString() == child.ToString());
                    if (firstOrDefault != null)
                    {
                        var limitLevel = firstOrDefault.Level;

                        if (includeChild)
                        {
                            treeParentDaos = _session.Query<TreeParentDao>()
                                .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                                .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                                .Where(tp => tp.Level <= limitLevel)
                                .OrderByDescending(tp => tp.Level)
                                .ToList();
                        }
                        else
                        {
                            treeParentDaos = _session.Query<TreeParentDao>()
                                .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                                .Where(tp => tp.TreeParent.Id.ToString() == child.ToString())
                                .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                                .Where(tp => tp.Level <= limitLevel)
                                .OrderByDescending(tp => tp.Level)
                                .ToList();
                        }
                    }
                    else
                    {
                        treeParentDaos = new List<TreeParentDao>();
                    }
                }
                else
                {
                    if (includeChild)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderByDescending(tp => tp.Level)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                            .Where(tp => tp.TreeParent.Id.ToString() == child.ToString())
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderByDescending(tp => tp.Level)
                            .ToList();
                    }
                }                

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeParentDao in treeParentDaos)
                {
                    var _treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent.Id.ToString() == treeParentDao.TreeParent.Id.ToString())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        Id = treeParentDao.TreeParent.Id,
                        Parent = treeParentDao.TreeParent.Parent,
                        Name = treeParentDao.TreeParent.Name,
                        ShortName = treeParentDao.TreeParent.ShortName,
                        Type = treeParentDao.TreeParent.Type,
                        State = treeParentDao.TreeParent.State,
                        CreateDateTime = treeParentDao.TreeParent.CreateDateTime,
                        HasChildren = _treeParentDaos.Count > 0
                    });
                }

                return virtualTreeDaos;
            }
        }
        
        public List<VirtualTreeDao> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds,
            List<Guid> ignoreTypeIds, Guid? parent = null)
        {
            using (var transaction = _session.StartTransaction())
            {
                ICriteria criteria;                

                if (parent == null)
                {                    
                    if (typeIds.IsEmpty())
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }
                        
                    }
                    else
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }                        
                    }
                }
                else
                {
                    if (typeIds.IsEmpty())
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }

                    }
                    else
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }
                    }
                }

                var treeDaos = criteria.List<TreeParentDao>().Select(ltp => ltp.TreeChild).Distinct().ToList();

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeDao in treeDaos)
                {
                    var treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent.Id.ToString() == treeDao.Id.ToString())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        Id = treeDao.Id,
                        Parent = treeDao.Parent,
                        Name = treeDao.Name,
                        ShortName = treeDao.ShortName,
                        Type = treeDao.Type,
                        State = treeDao.State,
                        CreateDateTime = treeDao.CreateDateTime,
                        HasChildren = treeParentDaos.Count > 0
                    });
                }

                return virtualTreeDaos;
            }
        }

        public TreeDao CreateTree(TreeDao treeDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(treeDao);

                transaction.Commit();                

                return _session.Query<TreeDao>().FirstOrDefault(t => t.Id.ToString() == treeDao.Id.ToString());
            }
        }

        public TreeDao GetTree(Guid treeId)
        {
            using (var transaction = _session.StartTransaction())
            {                
                return _session.Query<TreeDao>().FirstOrDefault(t => t.Id.ToString() == treeId.ToString());
            }
        }

        public void UpdateTree(TreeDao treeDao)
        {
            using (var transaction = _session.StartTransaction())
            {                
                _session.SaveOrUpdate(treeDao);                
                transaction.Commit();
            }
        }

        public UserDao CreateUser(UserDao userDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(userDao);

                transaction.Commit();

                return _session.Query<UserDao>().FirstOrDefault(p => p.Id.ToString() == userDao.Id.ToString());
            }
        }

        public UserDao GetUser(Guid userId)
        {
            using (var transaction = _session.StartTransaction())
            {                                
                return _session.Query<UserDao>().FirstOrDefault(u => u.Id.ToString() == userId.ToString());                
            }
        }

        public void UpdateUser(UserDao userDao)
        {
            using (var transaction = _session.StartTransaction())
            {                
                _session.SaveOrUpdate(userDao);                
                transaction.Commit();                
            }                        
        }

        public UserDao FindUserByLogin(string login)
        {
            using (var transaction = _session.StartTransaction())
            {                
                var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Login == login);
                return userDao;
            }
        }

        public UserDao FindUserByEmail(string email)
        {
            using (var transaction = _session.StartTransaction())
            {                
                var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Email == email);
                return userDao;
            }
        }

        public PersonDao CreatePerson(PersonDao personDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(personDao);

                transaction.Commit();

                return _session.Query<PersonDao>().FirstOrDefault(p => p.Id.ToString() == personDao.Id.ToString());
            }
        }

        public PersonDao GetPerson(Guid personId)
        {
            using (var transaction = _session.StartTransaction())
            {                
                return _session.Query<PersonDao>().FirstOrDefault(p => p.Id.ToString() == personId.ToString());
            }
        }

        public void UpdatePerson(PersonDao personDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(personDao);
                transaction.Commit();
            } 
        }

        public DocumentTypeDao CreateDocumentType(DocumentTypeDao documentTypeDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(documentTypeDao);

                transaction.Commit();

                return _session.Query<DocumentTypeDao>().FirstOrDefault(p => p.Id.ToString() == documentTypeDao.Id.ToString());
            }
        }

        public DocumentTypeDao GetDocumentType(Guid documentTypeId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<DocumentTypeDao>().FirstOrDefault(p => p.Id.ToString() == documentTypeId.ToString());
            }
        }

        public List<DocumentTypeDao> GetDocumentTypes()
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<DocumentTypeDao>().Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public void UpdateDocumentType(DocumentTypeDao documentTypeDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(documentTypeDao);
                transaction.Commit();
            }
        }

        public DocumentDao CreateDocument(DocumentDao documentDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(documentDao);

                transaction.Commit();

                return _session.Query<DocumentDao>().FirstOrDefault(p => p.Id.ToString() == documentDao.Id.ToString());
            }
        }

        public DocumentDao GetDocument(Guid documentId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<DocumentDao>().FirstOrDefault(p => p.Id.ToString() == documentId.ToString());
            }
        }

        public List<DocumentDao> GetDocuments()
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<DocumentDao>().Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public DocumentDao GetLastDocumentChange(Guid documentId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<DocumentDao>()
                    .Where(p => p.DocumentParent.Id.ToString() == documentId.ToString())
                    .Where(p => p.DocumentType.Parent.Id == SystemObjects.AllReportChangeTypes)
                    .OrderByDescending(p => p.CreateDateTime).FirstOrDefault();
            }
        }

        public void UpdateDocument(DocumentDao documentDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(documentDao);
                transaction.Commit();
            }
        }       

        public ReportTypeGroupDao CreateReportTypeGroup(ReportTypeGroupDao reportTypeGroupDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(reportTypeGroupDao);

                transaction.Commit();

                return _session.Query<ReportTypeGroupDao>().FirstOrDefault(p => p.Id.ToString() == reportTypeGroupDao.Id.ToString());
            }
        }

        public ReportTypeGroupDao GetReportTypeGroup(Guid reportTypeGroupId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportTypeGroupDao>().FirstOrDefault(p => p.Id.ToString() == reportTypeGroupId.ToString());
            }
        }

        public List<ReportTypeGroupDao> GetReportTypeGroups()
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportTypeGroupDao>().Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public void UpdateReportTypeGroup(ReportTypeGroupDao reportTypeGroupDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(reportTypeGroupDao);
                transaction.Commit();
            }
        }

        public ReportTypeDao CreateReportType(ReportTypeDao reportTypeDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(reportTypeDao);

                transaction.Commit();

                return _session.Query<ReportTypeDao>().FirstOrDefault(p => p.Id.ToString() == reportTypeDao.Id.ToString());
            }
        }

        public ReportTypeDao GetReportType(Guid reportTypeId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportTypeDao>().FirstOrDefault(p => p.Id.ToString() == reportTypeId.ToString());
            }
        }

        public List<ReportTypeDao> GetReportTypes()
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportTypeDao>().Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public void UpdateReportType(ReportTypeDao reportTypeDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(reportTypeDao);
                transaction.Commit();
            }
        }

        public ReportDao CreateReport(ReportDao reportDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(reportDao);

                transaction.Commit();

                return _session.Query<ReportDao>().FirstOrDefault(p => p.Id.ToString() == reportDao.Id.ToString());
            }
        }

        public ReportDao GetReport(Guid reportId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportDao>().FirstOrDefault(p => p.Id.ToString() == reportId.ToString());
            }
        }

        public List<ReportDao> GetReports()
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportDao>().Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public List<ReportDao> GetReportsByType(Guid reportTypeId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportDao>().
                    Where(p => p.ReportType.Id.ToString() == reportTypeId.ToString()).
                    Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public void UpdateReport(ReportDao reportDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(reportDao);
                transaction.Commit();
            }
        }

        public JuridicalPersonDao CreateJuridicalPerson(JuridicalPersonDao juridicalPersonDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(juridicalPersonDao);

                transaction.Commit();

                return _session.Query<JuridicalPersonDao>().FirstOrDefault(p => p.Id.ToString() == juridicalPersonDao.Id.ToString());
            }
        }

        public JuridicalPersonDao GetJuridicalPerson(Guid juridicalPersonId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<JuridicalPersonDao>().FirstOrDefault(p => p.Id.ToString() == juridicalPersonId.ToString());
            }
        }

        public List<JuridicalPersonDao> GetJuridicalPersons()
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<JuridicalPersonDao>().Where(p => p.State.ToString() != ObjectStates.osDeleted.ToString()).ToList();
            }
        }

        public void UpdateJuridicalPerson(JuridicalPersonDao juridicalPersonDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(juridicalPersonDao);
                transaction.Commit();
            }
        }

        public ReportDataDao CreateReportData(ReportDataDao reportDataDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(reportDataDao);
                transaction.Commit();
                return _session.Query<ReportDataDao>().FirstOrDefault(p => p.Id.ToString() == reportDataDao.Id.ToString());
            }
        }

        public ReportDataDao GetReportData(Guid reportDataId)
        {
            using (var transaction = _session.StartTransaction())
            {
                return _session.Query<ReportDataDao>().FirstOrDefault(p => p.Id.ToString() == reportDataId.ToString());
            }
        }

        public List<ReportDataDao> GetReportDataByReportAndPage(Guid reportId, int page)
        {
            using (var transaction = _session.StartTransaction())
            {
                return
                    _session.Query<ReportDataDao>()
                        .Where(p => p.Report.Id.ToString() == reportId.ToString())
                        .Where(p => p.Page == page)
                        .ToList();
            }
        }

        public void UpdateReportData(ReportDataDao reportDataDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(reportDataDao);
                transaction.Commit();
            }
        }

        public void DeleteReportData(Guid reportDataId)
        {
            using (var transaction = _session.StartTransaction())
            {
                var reportDataDao = _session.Query<ReportDataDao>().FirstOrDefault(p => p.Id.ToString() == reportDataId.ToString());
                _session.Delete(reportDataDao);
                transaction.Commit();
            }
        }

        public void DeleteReportDataByReport(Guid reportId)
        {
            using (var transaction = _session.StartTransaction())
            {
                var reportDataDaos = _session.Query<ReportDataDao>().Where(p => p.Report.Id.ToString() == reportId.ToString()).ToList();
                
                var queryString = string.Format("delete from l_report_data where group_id = :groupId");
                _session.CreateQuery(queryString)
                       .SetParameter("groupId", reportId)
                       .ExecuteUpdate();

                transaction.Commit();
            }
        }
    }
}
