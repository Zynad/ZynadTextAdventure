using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class WandRepository(IGameDatabase database) : BaseRepo<WandEntity>(database, db => db.Wands), IWandRepository;
