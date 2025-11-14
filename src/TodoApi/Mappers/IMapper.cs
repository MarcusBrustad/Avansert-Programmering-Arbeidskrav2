namespace TodoApi.Mappers;

public interface IMapper <TDto, TModel>
    where TDto : class
    where TModel : class
{
    TDto MapToDto(TModel model);
    TModel MapToModel(TDto dto);
}