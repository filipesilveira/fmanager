using FManager.Core;
using FManager.Data.Entities.Shared;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FManager.Units
{
    internal class RepositoryMocker<T>
        where T: BaseEntity
    {

        public IList<T> Db_mock { get; set; } = new List<T>();
        public IRepository<T> R_mock { get; private set; }

        public RepositoryMocker()
        {
            this.R_mock = GenerateServiceMock();
        }

        public RepositoryMocker(IList<T> db)
        {
            this.Db_mock = db;
            this.R_mock = GenerateServiceMock();
        }

        private IRepository<T> GenerateServiceMock()
        {
            var reportRepositoryMock = new Mock<IRepository<T>>();

            reportRepositoryMock
                .Setup(m => m.GetAllAsync())
                .Returns(() => Task.FromResult(this.Db_mock.ToList()));

            reportRepositoryMock
                .Setup(m => m.GetAllQueryableAsync())
                .Returns(() => Task.FromResult(this.Db_mock.AsQueryable()));
            
            reportRepositoryMock
                .Setup(m => m.GetAllQueryableNotTrackingAsync())
                .Returns(() => Task.FromResult(this.Db_mock.AsQueryable().AsNoTracking()));

            reportRepositoryMock
                .Setup(m => m.GetAllQueryableByCriteriaAsync(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => Task.FromResult(this.Db_mock.AsQueryable().Where(fitler)));
            reportRepositoryMock
                .Setup(m => m.GetAllQueryableByCriteriaNotTrackingAsync(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => Task.FromResult(this.Db_mock.AsQueryable().Where(fitler)));

            reportRepositoryMock
                .Setup(m => m.GetAllByCriteriaAsync(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => Task.FromResult(this.Db_mock.AsQueryable().Where(fitler).ToList()));

            reportRepositoryMock
                .Setup(m => m.GetByCriteriaAsync(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => Task.FromResult(this.Db_mock.AsQueryable().Where(fitler).FirstOrDefault()));
            reportRepositoryMock
                .Setup(m => m.GetByCriteriaNotTrackingAsync(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => Task.FromResult(this.Db_mock.AsQueryable().Where(fitler).FirstOrDefault()));

            reportRepositoryMock
                .Setup(m => m.Find(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => this.Db_mock.AsQueryable().Where(fitler).FirstOrDefault());
            reportRepositoryMock
                .Setup(m => m.FindAll(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns<Expression<Func<T, bool>>>((fitler) => this.Db_mock.AsQueryable().Where(fitler));


            reportRepositoryMock
                .Setup(m => m.Add(It.IsAny<T>()))
                .Callback<T>((entity) => this.Db_mock.Add(entity));
            reportRepositoryMock
                .Setup(m => m.Delete(It.IsAny<T>()))
                .Callback<T>((entity) => this.Db_mock.Remove(entity));
            reportRepositoryMock
                .Setup(m => m.Update(It.IsAny<T>()))
                .Callback<T>((entity) => Console.WriteLine($"{ entity.GetType().Name } Entity updated" + entity));

            return reportRepositoryMock.Object;
        }
    }
}
