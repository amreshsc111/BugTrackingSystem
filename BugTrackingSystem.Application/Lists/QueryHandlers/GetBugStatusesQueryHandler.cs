using BugTrackingSystem.Application.DTOs;
using BugTrackingSystem.Application.Lists.Queries;
using BugTrackingSystem.Domain.Enums;
using MediatR;
using System.ComponentModel;
using System.Reflection;

namespace BugTrackingSystem.Application.Lists.QueryHandlers
{
    public class GetBugStatusesQueryHandler : IRequestHandler<GetBugStatusesQuery, IEnumerable<BugStatusDto>>
    {
        public Task<IEnumerable<BugStatusDto>> Handle(GetBugStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = Enum.GetValues(typeof(GeneralEnums.BugStatus))
                .Cast<GeneralEnums.BugStatus>()
                .Select(s => new BugStatusDto
                {
                    Id = (int)s,
                    Name = GetEnumDescription(s)
                });

            return Task.FromResult(statuses);
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi == null) return value.ToString();

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
