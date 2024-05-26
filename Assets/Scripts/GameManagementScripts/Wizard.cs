using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private Queue<string> _speechQueue;
    [SerializeField] private Image _speechBubble;
    [SerializeField] private Text _speechText;
    void Awake()
    {
        _speechQueue = Dialog();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    public IEnumerator Move(Vector3 target, float speed, GameObject tutorial = null)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            _anim.SetBool("Walking", true);
            Vector3 newpos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            _rb.MovePosition(newpos);
            yield return null;
        }

        _anim.SetBool("Walking", false);
        if (tutorial != null)
        {
            TutorialManager tm = tutorial.GetComponent<TutorialManager>();
            tm.TutorialStep++;
            tm.StepActive = false;
        }
        yield return new WaitForFixedUpdate();
    }
    public void ToggleSpeechBubble(bool active)
    {
        _speechBubble.GetComponent<Image>().enabled = active;
        _speechText.GetComponent<Text>().enabled = active;
    }
    public void NextSentence()
    {
        _speechText.GetComponent<Text>().text = _speechQueue.Dequeue();
    }
    private Queue<string> Dialog()
    {
        Queue<string> dialog = new Queue<string>();
        dialog.Enqueue("Xin chào, chào mừng bạn đến với Xylesia! Tôi là phù thủy! Nhấn 'T' để nói chuyện với tôi!\n..."); // Step 2
        dialog.Enqueue("Tôi ở đây để giúp bạn bắt đầu.\n..."); // Step 3
        dialog.Enqueue("Bạn có thể di chuyển xung quanh bằng cách nhấn các phím mũi tên.\n..."); // Step 4
        dialog.Enqueue("Wow, trông thật sống động!\n..."); // Step 5
        dialog.Enqueue("Bạn cũng có thể nhảy bằng cách nhấn phím cách.\n..."); // Step 6
        dialog.Enqueue("Thật tuyệt!\n..."); // Step 7
        dialog.Enqueue("Bây giờ, thế giới này có thể rất nguy hiểm nên bạn sẽ cần phải tự vệ\n..."); // Step 8
        dialog.Enqueue("Bạn có 3 cách tấn công khác nhau.\n..."); // Step 9
        dialog.Enqueue("Đầu tiên là đòn tấn công cơ bản. Nhấn 'Nhấp chuột trái'.\n..."); // Step 10
        dialog.Enqueue("Thứ hai là một cuộc tấn công đặc biệt. Nhấn 'Nhấp chuột phải'.\n..."); // Step 11
        dialog.Enqueue("Thứ ba là phép thuật của bạn. Nhấn 'F'.\n..."); // Step 12
        dialog.Enqueue("Một số kĩ năng của bạn tiêu tốn mana.\n..."); // Step 13
        dialog.Enqueue("Bạn có thể thấy mana của mình (thanh màu xanh) ở góc trên cùng bên trái.\n..."); // Step 14
        dialog.Enqueue("Trên mana của bạn là lượng máu của bạn (thanh màu đỏ).\n..."); // Step 15
        dialog.Enqueue("Mana là một nguồn tài nguyên phép thuật đến từ mọi nơi!\n...");
        dialog.Enqueue("Nó sẽ từ từ trở lại với bạn theo thời gian.\n...");
        dialog.Enqueue("Thế giới này cũng đầy những trái cây phép thuật!\n...");
        dialog.Enqueue("Một số kẻ thù sẽ để lại những trái cây mà chúng đang ẩn giữ.\n...");
        dialog.Enqueue("Bạn có thể ăn những trái cây bằng cách nhấp vào chúng trong Túi đồ của mình.\n...");
        dialog.Enqueue("Đây là một số trái cây mà bạn có thể ăn.\n...");
        dialog.Enqueue("Bây giờ, bạn cũng có thể sử dụng mana của mình để nhảy cao hơn!\n...");
        dialog.Enqueue("Nhấn nhảy một lần nữa khi đang ở giữa không trung để mana nhảy!\n...");
        dialog.Enqueue("Bạn cũng có thể sử dụng mana của mình để chạy nhanh hơn!\n...");
        dialog.Enqueue("Nhấn 'Shift' để mana chạy!\n...");
        dialog.Enqueue("Ok, tôi nghĩ đó là tất cả...\nÔi chờ đã, có một điều cuối cùng!\n...");
        dialog.Enqueue("Bạn cũng có thể chặn các cuộc tấn công của kẻ thù! Nhấn 'R' để chặn.\n...");
        dialog.Enqueue("Được rồi! Đó là tất cả những gì tôi có thể dạy bạn lúc này.\n...");
        dialog.Enqueue("Đi sang bên phải để bắt đầu cuộc phiêu lưu của bạn. Chúc may mắn!\n...");
        return dialog;
    }
}
