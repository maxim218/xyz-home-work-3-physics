using System;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {
    private float _directionX = 0f;
    private float _directionY = 0f;
    
    public void SetDirectionX(float value) {
        _directionX = value;
    }

    public void SetDirectionY(float value) {
        _directionY = value;
    }

    private Rigidbody2D _rigidbody2D = null;

    [SerializeField] private float horizontalSpeed = 0f;
    [SerializeField] private float forceVertical = 0f;

    public void SetForceVertical(float val)
    {
        forceVertical = val;
    }

    public void SetHorizontalSpeed(float hor)
    {
        horizontalSpeed = hor;
    }

    public void PerkImproveHorizontalSpeed(float value)
    {
        horizontalSpeed += value;
    }

    public void PerkImproveForceVertical(float value)
    {
        forceVertical += value;
    }
    
    private HeroAnimationControl _heroAnimationControl = null;
    private SpriteRenderer _spriteRenderer = null;
    private ControlHealth _controlHealth = null;
    
    private void Start() {
        _controlHealth = gameObject.GetComponent<ControlHealth>();
        _heroAnimationControl = gameObject.GetComponent<HeroAnimationControl>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void ControlFlipHorizontal() {
        if (_directionX > 0) 
            _spriteRenderer.flipX = false;
        else if (_directionX < 0) 
            _spriteRenderer.flipX = true;
    }

    private void ControlAnimationState(bool isGroundedHero) {
        if (isGroundedHero == false) {
            const string key = "jump";
            _heroAnimationControl.AnimationHeroPlay(key);
        } else if (_directionX != 0) {
            const string key = "run";
            _heroAnimationControl.AnimationHeroPlay(key);
        } else {
            const string key = "idle";
            _heroAnimationControl.AnimationHeroPlay(key);
        }
    }
    
    private void FixedUpdate() {
        // hero dead
        int heroLiveInt = _controlHealth.Lives;
        if (heroLiveInt <= 0) {
            _rigidbody2D.velocity = Vector2.zero;
            _spriteRenderer.sprite = deadSprite;
            if(gameObject.GetComponent<Animator>()) Destroy(GetComponent<Animator>());
            return;
        }
        
        // flip x control
        ControlFlipHorizontal();
        
        // horizontal move
        float x = _directionX * horizontalSpeed;
        float y = _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = new Vector2(x, y);
        
        // значения по оси X для левого края героя, середины, правого края героя
        float leftX = transform.position.x - 0.2f;
        float middleX = transform.position.x;
        float rightX = transform.position.x + 0.2f;
        // позиция по оси Y чуть ниже ступней героя
        float posY = transform.position.y - 0.6f;
            
        // получаем 3 точки, из которых будут выходить проверочные векторы
        _leftVector = new Vector2(leftX, posY);
        _middleVector = new Vector2(middleX, posY);
        _rightVector = new Vector2(rightX, posY);
        
        // из каждой точки опускаем проверочный вектор длиной LengthControlling
        // возвращаемый флаг будет True, если проверочный вектор коснется платформы
        _flagLeft = BottomControl(_leftVector, LengthControlling);
        _flagMiddle = BottomControl(_middleVector, LengthControlling);
        _flagRight = BottomControl(_rightVector, LengthControlling);
        
        // control animation state
        ControlAnimationState(_flagLeft || _flagMiddle || _flagRight);
        
        // vertical jump
        if (!(_directionY > 0)) return;
        // хотя бы один проверочный вектор должен коснуться платформы
        bool existsGoodFlag = (_flagLeft || _flagMiddle || _flagRight);
        if (!existsGoodFlag) return;
        if (_rigidbody2D.velocity.y > 0) return;
        _rigidbody2D.AddForce(Vector2.up * forceVertical, ForceMode2D.Impulse);
    }

    public float GetVelocityHorizontal() {
        return _rigidbody2D.velocity.x;
    }

    public float GetVelocityVertical() {
        return _rigidbody2D.velocity.y;
    }
    
    public bool IsHeroStayOnGround() {
        return (_flagLeft || _flagMiddle || _flagRight);
    }

    [SerializeField] private Sprite deadSprite = null;
    
    // проверочные векторы для контроля касания платформ
    private Vector2 _leftVector = Vector2.zero;
    private Vector2 _middleVector = Vector2.zero;
    private Vector2 _rightVector = Vector2.zero;
    
    // длина проверочных векторов
    private const float LengthControlling = 0.2f;

    // состояния - касается ли в данный момент проверочный вектор платформы
    private bool _flagLeft = false;
    private bool _flagMiddle = false;
    private bool _flagRight = false;

    private static bool BottomControl(Vector2 startPosition, float distanceFloat) {
        // бросаем луч вниз и выходим, если луч никого не коснулся
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, distanceFloat);
        if (!hit) return false;
        // если мы здесь, значит луч чего-то коснулся
        // проверяем на объекте касания наличие скрипта Ground
        GameObject bottomObject = hit.transform.gameObject;
        Ground script = bottomObject.GetComponent<Ground>();
        return script;
    }

    private void OnDrawGizmos() {
        // рисуем для отладки проверочные векторы длины LengthControlling
        Debug.DrawRay(_leftVector, Vector2.down * LengthControlling, GetColor(_flagLeft));
        Debug.DrawRay(_middleVector, Vector2.down * LengthControlling, GetColor(_flagMiddle));
        Debug.DrawRay(_rightVector, Vector2.down * LengthControlling, GetColor(_flagRight));
    }

    private static Color GetColor(bool condition) {
        // get good or bad color
        return condition ? Color.green : Color.red;
    }
}
