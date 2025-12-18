using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class AxeRepository(IGameDatabase database) : BaseRepo<AxeEntity>(database, db => db.Axes), IAxeRepository;
