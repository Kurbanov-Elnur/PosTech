using GalaSoft.MvvmLight.Messaging;
using PostTech.Messages;
using PostTech.Services.Interfaces;

namespace PostTech.Services.Classes;

class DataService : IDataService
{
    private readonly IMessenger _messenger;

    public DataService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void SendData(object data)
    {
        _messenger.Send(new DataMessage()
        {
            Data = data
        });
    }
}