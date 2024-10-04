using databasepmapilearn6.InputModels;
using databasepmapilearn6.Utilities;

namespace databasepmapilearn6.Domains.Utilities;

public interface IUtlEmail
{
    // cari tau bacanya
    void Send(UtlLogger logger, IMEmail.Message message);

    void Send(UtlLogger logger, List<IMEmail.Message> messages);
}