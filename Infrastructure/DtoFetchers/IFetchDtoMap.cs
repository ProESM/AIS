using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using DTO;

namespace Infrastructure.DtoFetchers
{
    /// <summary>
    /// Маппинг, устанавливающий отношения между свойствами объектов, находящихся в хранилище,
    /// и свойствами извлекаемых объектов.
    /// </summary>
    /// <typeparam name="TSource">Тип данных в хранилище</typeparam>
    /// <typeparam name="TTarget">Тип извлекаемых данных</typeparam>
    public interface IFetchDtoMap<TSource, TTarget>
        where TSource : IEntity
        where TTarget : BaseDto
    {
        /// <summary>
        /// Устанавливает соответствие между свойствами хранимого и извлекаемого объектов.
        /// </summary>
        /// <typeparam name="TProperty">Тип свойства</typeparam>
        /// <param name="targetProperty">Лямбда-выражение доступа к свойству извлекаемого объекта (Например, dto => dto.Id)</param>
        /// <param name="sourceProperty">Лямбда-выражение доступа к свойству хранимого объекта (Например, a => a.Id)</param>
        /// <returns>Объект этого же маппинга</returns>
        IFetchDtoMap<TSource, TTarget> Map<TProperty>(
            Expression<Func<TTarget, TProperty>> targetProperty,
            Expression<Func<TSource, TProperty>> sourceProperty);

        /// <summary>
        /// Добавляет делегат со сложной логикой маппинга свойств (коллекции, составные свойства и т.п.)
        /// </summary>
        /// <param name="fetchOperation">Делегат; первый параметр - LINQ-запрос по объектам в хранилище; второй - список загруженных DTO</param>
        /// <returns></returns>
        IFetchDtoMap<TSource, TTarget> CustomMap(Action<IQueryable<TSource>, IEnumerable<TTarget>> fetchOperation);
    }
}
