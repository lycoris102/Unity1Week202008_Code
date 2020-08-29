using System;
using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Entity;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.UseCase.Lobby.Interface;
using Lycoris102.Unity1Week202008.View.Lobby;
using Lycoris102.Unity1Week202008.View.Lobby.Interface;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Lobby
{
    public class NameUseCase : IInitializable, IDisposable, INameUseCase
    {
        private INameEntity NameEntity { get; }
        private IReadOnlyList<INameRenderer> NameRendererList { get; }
        private INameHandler NameHandler { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public NameUseCase(INameEntity nameEntity,
            IReadOnlyList<INameRenderer> nameRendererList,
            INameHandler nameHandler)
        {
            NameEntity = nameEntity;
            NameRendererList = nameRendererList;
            NameHandler = nameHandler;
        }

        public void Initialize()
        {
            NameHandler.OnSetNameAsObservable()
                .Subscribe(name =>
                {
                    Set(name);
                    SetPhoton(name);
                    Render(name);
                })
                .AddTo(Disposable);

            Render(NameEntity.Load());
            SetPhoton(NameEntity.Load());
        }

        private void Set(string name)
            => NameEntity.Save(name);

        private void Render(string name)
        {
            foreach (var nameRenderer in NameRendererList)
            {
                nameRenderer.Render(name);
            }
        }

        private void SetPhoton(string name)
            => PhotonNetwork.NickName = name;
    }
}