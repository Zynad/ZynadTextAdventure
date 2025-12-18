using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class GlovesRepository(IGameDatabase database)
    : BaseRepo<GlovesEntity>(database, db => db.Gloves), IGlovesRepository;
