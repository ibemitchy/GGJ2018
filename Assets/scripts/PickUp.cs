using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component = System.ComponentModel.Component;

public class PickUp : MonoBehaviour
{
//     private GameObject pickUpAnimation;
    private Collider _player;
    private int _healthPackAmount; // TODO: health should be float
    private List<Action> _effects;

    // Use this for initialization
    void Start()
    {
        _healthPackAmount = 10;
        _effects = new List<Action>();
        AddEffects();
    }

    // initiates list of effect functions
    void AddEffects()
    {
        _effects.Add(GiveCure);
        _effects.Add(GiveInfectionToAll);
        _effects.Add(SpawnTrap);
        _effects.Add(GiveSpeedBuff);
        _effects.Add(GiveHomingProjectile);
        _effects.Add(GiveRicochetProjectile);
        _effects.Add(GiveInvincibility);
        _effects.Add(GiveInvisibility);
        _effects.Add(Teleport);
        _effects.Add(MapChange);
        _effects.Add(GiveReverseControl);
        _effects.Add(GiveDrunkMode);
        _effects.Add(GiveHealth);
    }

    // collision detection
    void OnTriggerEnter(Collider collider)
    {
        if (!collider.Equals(null) && collider.CompareTag("Player"))
        {
            _player = collider;
            TriggerEffect();
        }
    }

    // randomly triggers an effect
    void TriggerEffect()
    {
//         Instantiate(pickUpAnimation, transform.position, transform.rotation);

        System.Random random = new System.Random();
        int randomNum = random.Next(_effects.Count);
        Debug.Log(randomNum);
        _effects[randomNum]();
//        Destroy(gameObject);
    }

    // decreases infection count of colliding player by 1
    void GiveCure()
    {
        Infection infection = _player.GetComponent<Infection>();
        infection.DecrementInfectionNumber();
        // TODO: decrement needs to set infectedFlag to false when 0, increment needs to reset infectedFlag to true
    }

    // increments infection count of all players by 1
    void GiveInfectionToAll()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            Infection infection = player.GetComponent<Infection>();
            infection.IncrementInfectionNumber();
        }
    }

    // randomly spawns a trap in the maze
    void SpawnTrap()
    {
        // TODO: maze trap spawner
//        Maze maze = Maze.GetComponent<Grid>();
//        maze.spawnTrap();
    }

    // increases player movement speed
    void GiveSpeedBuff()
    {
        PlayerMovementControl movement = _player.GetComponent<PlayerMovementControl>();
//        movement.IncrementSpeed(); // TODO: add increment method to PlayerMovementControl
    }

    // gives player homing projectiles
    void GiveHomingProjectile()
    {
        Projectile projectile = _player.GetComponent<Projectile>();
//        projectile.SetHoming(true); // TODO: add homing boolean to Projectile
    }

    // gives player projectiles that ricochet
    void GiveRicochetProjectile()
    {
        Projectile projectile = _player.GetComponent<Projectile>();
//        projectile.SetRicochet(true); // TODO: add ricochet boolean to Projectile
    }

    // player becomes immune to infection and infection damage
    void GiveInvincibility()
    {
        Health health = _player.GetComponent<Health>();
//        health.SetInvincibility(true); // TODO: add invincibility boolean to Health
    }

    // TODO: this needs to be tested/modified for multiplayer
    // player becomes invisible
    void GiveInvisibility()
    {
        _player.GetComponent<MeshRenderer>().enabled = false;

        // TODO: put veil around player, and add NetworkIdentity
//        if (isLocalPlayer)
//        {
//            player.GetComponent<MeshRenderer>().enabled = true;
//        }
    }

    // teleports player to random location
    void Teleport()
    {
        // TODO: get randomvector generator from maze
//        player.transform.position = Maze.GetRandomVector();
    }

    // forces terrain change
    void MapChange()
    {
        // TODO: change maps: terrain change, poison mist map, obstacle-free free-for-all map, ...
    }

    // reverses player control
    void GiveReverseControl()
    {
        PlayerMovementControl movement = _player.GetComponent<PlayerMovementControl>();
//        movement.SetReverse(true); // TODO: add reverse boolean to PlayerMovementControl
    }

    // TODO: drunk mode, jitter camera, limited vision, render trippy screen?
    void GiveDrunkMode()
    {
    }

    // heals the target player
    void GiveHealth()
    {
        Health health = _player.GetComponent<Health>();
        health.giveHeal(_healthPackAmount); // TODO: giveHeal to GiveHealth (?)
    }

    // returns the colliding player
    public Collider getPlayer()
    {
        return _player;
    }
}
