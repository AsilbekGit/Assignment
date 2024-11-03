using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandling : MonoBehaviour
{
    public float interactionDistance = 2.0f;
    public LayerMask boxLayer;
    private PlayerInput playerInput;

    // Audio sources for interaction sound and background music
    private AudioSource interactionAudioSource;
    private AudioSource backgroundAudioSource;

    // Sound clips
    public AudioClip interactSound;        // Sound to play on interaction
    public AudioClip backgroundMusic;      // Background music clip

    void Start()
    {
        // Set up player input
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Interact"].performed += OnInteract;

        // Get AudioSources (make sure you have added two AudioSource components to this object)
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            interactionAudioSource = audioSources[0];  // Use first AudioSource for interaction sounds
            backgroundAudioSource = audioSources[1];   // Use second AudioSource for background music
        }

        // Play background music
        if (backgroundMusic != null && backgroundAudioSource != null)
        {
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.loop = true;  // Ensure the music loops
            backgroundAudioSource.Play();
        }
    }

    void OnDestroy()
    {
        playerInput.actions["Interact"].performed -= OnInteract;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, boxLayer))
        {
            if (hit.collider != null)
            {
                Debug.Log("Interacted with " + hit.collider.name);
                
                // Play interaction sound
                if (interactSound != null && interactionAudioSource != null)
                {
                    interactionAudioSource.PlayOneShot(interactSound);
                }

                // Interaction logic: Destroy the interacted object
                Destroy(hit.collider.gameObject); // Make the box disappear
            }
        }
    }
}
