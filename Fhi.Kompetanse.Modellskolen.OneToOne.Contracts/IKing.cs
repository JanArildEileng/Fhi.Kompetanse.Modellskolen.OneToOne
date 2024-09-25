using Refit;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.Contracts;

public interface IKing
{
    [Get("/api/King")]
    Task<List<GetKingDto>> GetKings();

    [Post("/api/King")]
    Task<GetKingDto> PostKing(PostKingDto postKingDto);

    [Delete("/api/King/{id}")]
    Task DeleteKing(int id);
}