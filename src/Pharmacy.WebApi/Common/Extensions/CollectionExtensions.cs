using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Helpers;

namespace Pharmacy.WebApi.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IQueryable<TSource> sources,
        PaginationParams? parameters)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("total-count"))
                HttpContextHelper.ResponseHeaders.Remove("total-count");

            HttpContextHelper.ResponseHeaders.Add("total-count", $"{sources.Count()}");

            return parameters is { PageSize: > 0, PageIndex: > 0 }
                ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
                : sources;
        }

        public static IQueryable<TSource> ToPagedAsQueryable<TSource>(this IQueryable<TSource> sources,
            PaginationParams? parameters)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("total-count"))
                HttpContextHelper.ResponseHeaders.Remove("total-count");

            HttpContextHelper.ResponseHeaders.Add("total-count", $"{sources.Count()}");

            return parameters is { PageSize: > 0, PageIndex: > 0 }
                ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
                : sources;
        }

        public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IEnumerable<TSource> sources,
            PaginationParams? parameters)
            => parameters is { PageSize: > 0, PageIndex: > 0 }
                ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
                : sources;
    }
}
