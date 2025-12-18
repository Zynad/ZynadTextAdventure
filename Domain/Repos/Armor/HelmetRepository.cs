using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class HelmetRepository(IGameDatabase database)
    : BaseRepo<HelmetEntity>(database, db => db.Helmets), IHelmetRepository;
