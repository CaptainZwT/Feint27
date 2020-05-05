// This will control the logic for each character. 
// Driving the various pices that make up either the player, or the NPC

using UnityEngine;

namespace Assets.Scripts.CharacterComponents
{
    class CharacterControl : MonoBehaviour
    {
        public SpellHandling spellHandling;
        public Character character;
        public Rigidbody2D rb;

        // parameters
        private bool isFacingRight = true;
        private bool isFlying = false;
        private Vector2 vel = Vector2.zero;
        private float horiz_speed = 0f;
        private float vert_speed = 0f;
        private float accel_mitigation = 4f;

        // Serialized Parameters
        [SerializeField] private bool debugging = true;
        [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;	// How much to smooth out the movement


        public void Awake()
        {
            // getting character class
            character = GetComponent<Character>();
            rb = GetComponent<Rigidbody2D>();

            spellHandling = new SpellHandling(character.pouch);
        }

        public void FixedUpdate()
        {
            // First call HandleSpeed to calculate move
            HandleSpeed();

            // then call Move to actually use that information to drive the character
            Move();
        }

        public void Move ()
        {

            // code from Brackeys tutorials to handle not only basic movement but also flipping of character
            if (horiz_speed > 0 && !isFacingRight)
            {
                // flip the player
                Flip();
            }
            else if (horiz_speed < 0 && isFacingRight)
            {
                // flip the player
                Flip();
            }

            Vector2 targetVelocity = new Vector2(10 * horiz_speed, 10 * vert_speed);

            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref vel, movementSmoothing);
        }

        // Helper Functions

        // Function taken from Brackeys tutorial, saving time.
        // helps Character movement by flipping a character.
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            isFacingRight = !isFacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void HandleSpeed ()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            //Debug.Log(vertical);

            // handle flying
            float effective_vertspeed = (character.engineFlightAccel / accel_mitigation);
            if (vertical > 0 && character.mana > effective_vertspeed) 
            {
                vert_speed += (effective_vertspeed * vertical * Time.fixedDeltaTime);

                // paying the mana toll for flight
                character.mana -= effective_vertspeed;
            }
            else if (vert_speed > 0)
            {
                vert_speed -= effective_vertspeed;
                if (vert_speed < 0) { vert_speed = 0; }
            }

            // handle normal movement
            if (horizontal != 0)
            {
                horiz_speed += ((character.engineAcceleration / accel_mitigation) * horizontal * Time.fixedDeltaTime);
            }
            else
            {
                horiz_speed = 0;
            }
        }
    }
}
