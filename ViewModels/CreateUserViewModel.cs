using Yonazone.Models;
using Yonazone.Data;

namespace Yonazone.ViewModels
{
  public class CreateUserViewModel : BaseViewModel
  {
    public User User {get;set;}
    public CreateUserViewModel(YonazoneContext ctx) : base(ctx) { }
  }
}