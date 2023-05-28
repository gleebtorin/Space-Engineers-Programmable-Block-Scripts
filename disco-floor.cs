List<IMyTerminalBlock> lightPanels = new List<IMyTerminalBlock>();
IMyBlockGroup discoLights;

public Program()
{
    // Set the update frequency to once every 10 minutes
    Runtime.UpdateFrequency = UpdateFrequency.Update100 | UpdateFrequency.Once;
}

void Main(string argument)
{
    // Get the 'Disco' group
    discoLights = GridTerminalSystem.GetBlockGroupWithName("Disco");
    if (discoLights == null)
    {
        Echo("Error: 'Disco' group not found!");
        return;
    }

    // Get all the Light Panels in the 'Disco' group
    discoLights.GetBlocksOfType(lightPanels);
    // Iterate through all the Light Panels
    foreach (IMyTerminalBlock lightPanel in lightPanels)
    {
        // Set the color of the Light Panel to a random fully saturated color. 25% brightness to avoid HDR issues.
        float hue = RandomFloat(0f, 359f);
        Color color = ColorFromHSV(hue, 1f, 0.25f);
        lightPanel.SetValue("Color", color);
    }
}

// Converts a hue, saturation, and value to a color
Color ColorFromHSV(float hue, float saturation, float value)
{
    // Calculate the RGB values of the color
    int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
    float f = hue / 60 - (float)Math.Floor(hue / 60);

    value = value * 255;
    int v = Convert.ToInt32(value);
    int p = Convert.ToInt32(value * (1 - saturation));
    int q = Convert.ToInt32(value * (1 - f * saturation));
    int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

    switch (hi)
    {
        case 0:
            return Color.FromNonPremultiplied(v, t, p, 255);
        case 1:
            return Color.FromNonPremultiplied(q, v, p, 255);
        case 2:
            return Color.FromNonPremultiplied(p, v, t, 255);
        case 3:
            return Color.FromNonPremultiplied(p, q, v, 255);
        case 4:
            return Color.FromNonPremultiplied(t, p, v, 255);
        default:
            return Color.FromNonPremultiplied(v, p, q, 255);
    }
}

float RandomFloat(float min, float max)
{
    // Generate a random float between min and max
    return (float)rnd.NextDouble() * (max - min) + min;
}

Random rnd = new Random();