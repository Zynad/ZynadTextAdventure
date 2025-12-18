using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class BootsRepository(IGameDatabase database) : BaseRepo<BootsEntity>(database, db => db.Boots), IBootsRepository;
