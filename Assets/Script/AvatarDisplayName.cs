using Photon.Pun;
using TMPro;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarDisplayName : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        var nameLabel = GetComponent<Text>();
        // �v���C���[���ƃv���C���[ID��\������
        nameLabel.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
    }
}