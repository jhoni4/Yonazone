using Yonazone.Models;
using Yonazone.Data;

namespace Yonazone.ViewModels
{
  /**
 * Class: Confirmation message
 * Purpose: Create confirmation message for the buyer after complete payment
 * Author: yona
 * Properties:
 *   PaymentTypeId - generated by the database upon creation
 */
  public class ConfirmationViewModel : BaseViewModel
  {
    public int OrderId {get;set;}
    public PaymentType PaymentType {get; set;}
    public ConfirmationViewModel(YonazoneContext ctx) : base(ctx) { }
  }
}

