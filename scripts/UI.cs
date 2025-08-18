using Godot;
using System;

public partial class UI : Control
{
	int moveCount = -1;
	[Export]
	int ammoCount;
	int lastAmmoCount;

	RichTextLabel movesLabel;
	HBoxContainer ammoContainer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		movesLabel = GetNode("Moves Counter").GetNode<RichTextLabel>("RichTextLabel");
		ammoContainer = GetNode<HBoxContainer>("Base Ammo Container");
		// setAmmoCount(4);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		movesLabel.Text = "Moves: " + moveCount;
		if (ammoCount != lastAmmoCount)
		{
			int index = lastAmmoCount - ammoCount - 1;
			ammoContainer.GetChild<TextureRect>(index).Texture = (Texture2D)ResourceLoader.Load("res://Scenes/UI/EmptyBoomerang.tres");
			// lastAmmoCount = ammoCount;
		}
	}


	public void setAmmoCount(int ammo)
	{
		ammoCount = ammo;
		for (int i = 0; i < ammoCount; i++)
		{
			TextureRect textureRect = new TextureRect();
			textureRect.Texture = (Texture2D)ResourceLoader.Load("res://Scenes/UI/FilledBoomerang.tres");
			// GD.Print(ResourceLoader.Load("res://Scenes/UI/FilledBoomerang.tres"));
			ammoContainer.AddChild(textureRect);
		}
		lastAmmoCount = ammoCount;
	}

	public void decrementAmmo()
	{
		ammoCount -= 1;
	}

	public void setMoveAmount(int move)
	{
		moveCount = move;
	}

	public void decrementMove()
	{
		moveCount -= 1;
	}
}
