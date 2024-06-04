

using Data.Entities;
using Data.Models;
using GraphQL.Types;

namespace Data.GraphQL.ObjestTypes;

public class CourseType : ObjectType<CourseEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CourseEntity> descriptor)
    {
        descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
        descriptor.Field(x => x.IsBestseller).Type<BooleanType>();
        descriptor.Field(x => x.IsDigital).Type<BooleanType>();
        descriptor.Field(x => x.IsBookmarked).Type<BooleanType>();
        descriptor.Field(x => x.Categories).Type<ListType<StringType>>();
        descriptor.Field(x => x.Title).Type<StringType>();
        descriptor.Field(x => x.Ingress).Type<StringType>();
        descriptor.Field(x => x.StarRating).Type<DecimalType>();
        descriptor.Field(x => x.Reviews).Type<StringType>();
        descriptor.Field(x => x.Likes).Type<StringType>();
        descriptor.Field(x => x.LikesInProcent).Type<StringType>();
        descriptor.Field(x => x.Hours).Type<StringType>();
        descriptor.Field(x => x.ImageUri).Type<StringType>();
        descriptor.Field(x => x.ImageHeaderUri).Type<StringType>();
        descriptor.Field(x => x.Authors).Type<ListType<AuthorType>>();
        descriptor.Field(x => x.Prices).Type<PricesType>();
        descriptor.Field(x => x.Content).Type<ContentType>();
    }
}

public class AuthorType : ObjectType<AuthorEntity>
{
    protected override void Configure(IObjectTypeDescriptor<AuthorEntity> descriptor)
    {
        descriptor.Field(y => y.Name).Type<StringType>();
        descriptor.Field(y => y.AuthorImg).Type<StringType>();
    }
}

public class PricesType : ObjectType<PriceEntity>
{
    protected override void Configure(IObjectTypeDescriptor<PriceEntity> descriptor)
    {
        descriptor.Field(a => a.Price).Type<DecimalType>();
        descriptor.Field(a => a.Discount).Type<DecimalType>();
        descriptor.Field(a => a.Currency).Type<StringType>();
    }
}

public class ContentType : ObjectType<ContentEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ContentEntity> descriptor)
    {
        descriptor.Field(c => c.Description).Type<StringType>();
        descriptor.Field(c => c.Includes).Type<ListType<StringType>>();
        descriptor.Field(c => c.ProgramDetails).Type<ListType<ProgramDetailType>>();
    }
}

public class ProgramDetailType : ObjectType<ProgramDetailEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ProgramDetailEntity> descriptor)
    {
        descriptor.Field(y => y.Id).Type<IntType>();
        descriptor.Field(y => y.Title).Type<StringType>();
        descriptor.Field(y => y.Description).Type<StringType>();
    }
}


public class UserCoursesInputType : ObjectType<UserCoursesEntity>
{
    protected override void Configure(IObjectTypeDescriptor<UserCoursesEntity> descriptor)
    {
        descriptor.Field(u => u.UserId).Type<StringType>();
        descriptor.Field(u => u.CourseId).Type<StringType>();
    }
}

public class UserCoursesResponseType : ObjectGraphType<UserCourses>
{
    public UserCoursesResponseType()
    {
        Field(b => b.UserId);
        Field(b => b.CourseId);
    }
}