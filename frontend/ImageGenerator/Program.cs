
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string projectPath = Directory.GetCurrentDirectory();

        string outputPath = Path.GetFullPath(
            Path.Combine(projectPath, "..", "frontend", "public", "images")
        );

        Directory.CreateDirectory(outputPath);

        Dictionary<string, string> images = new()
        {
            ["default-avatar.svg"] = """
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 240 240">
                <defs>
                    <linearGradient id="bg" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#DBEAFE"/>
                        <stop offset="1" stop-color="#EDE9FE"/>
                    </linearGradient>
                    <linearGradient id="person" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#3B82F6"/>
                        <stop offset="1" stop-color="#7C3AED"/>
                    </linearGradient>
                </defs>
                <circle cx="120" cy="120" r="115" fill="url(#bg)"/>
                <circle cx="120" cy="91" r="43" fill="url(#person)"/>
                <path d="M37 218c7-53 36-79 83-79s76 26 83 79"
                      fill="url(#person)"/>
                <circle cx="188" cy="48" r="12" fill="#FFFFFF" opacity=".7"/>
                <circle cx="48" cy="165" r="8" fill="#FFFFFF" opacity=".8"/>
            </svg>
            """,

            ["empty-projects.svg"] = """
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 480 340">
                <defs>
                    <linearGradient id="bg" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#EFF6FF"/>
                        <stop offset="1" stop-color="#EDE9FE"/>
                    </linearGradient>
                    <linearGradient id="folder" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#60A5FA"/>
                        <stop offset="1" stop-color="#6366F1"/>
                    </linearGradient>
                    <filter id="shadow" x="-30%" y="-30%" width="160%" height="180%">
                        <feDropShadow dx="0" dy="12" stdDeviation="12"
                                      flood-color="#64748B" flood-opacity=".2"/>
                    </filter>
                </defs>
                <circle cx="240" cy="165" r="135" fill="url(#bg)"/>
                <circle cx="90" cy="100" r="9" fill="#A78BFA"/>
                <circle cx="382" cy="220" r="13" fill="#93C5FD"/>
                <g filter="url(#shadow)">
                    <path d="M112 112h91l23 25h142v111H112z" fill="url(#folder)"/>
                    <path d="M112 137h256v111H112z" fill="#FFFFFF" opacity=".95"/>
                    <rect x="145" y="165" width="188" height="12" rx="6" fill="#BFDBFE"/>
                    <rect x="145" y="191" width="130" height="12" rx="6" fill="#DDD6FE"/>
                    <rect x="145" y="217" width="158" height="12" rx="6" fill="#E2E8F0"/>
                </g>
                <circle cx="348" cy="105" r="34" fill="#7C3AED"/>
                <path d="M348 88v34M331 105h34"
                      stroke="white" stroke-width="7" stroke-linecap="round"/>
            </svg>
            """,

            ["empty-tasks.svg"] = """
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 480 340">
                <defs>
                    <linearGradient id="bg" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#EFF6FF"/>
                        <stop offset="1" stop-color="#F5F3FF"/>
                    </linearGradient>
                    <linearGradient id="paper" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#FFFFFF"/>
                        <stop offset="1" stop-color="#EEF2FF"/>
                    </linearGradient>
                    <linearGradient id="check" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#3B82F6"/>
                        <stop offset="1" stop-color="#8B5CF6"/>
                    </linearGradient>
                    <filter id="shadow">
                        <feDropShadow dx="0" dy="12" stdDeviation="12"
                                      flood-color="#64748B" flood-opacity=".2"/>
                    </filter>
                </defs>
                <circle cx="240" cy="165" r="135" fill="url(#bg)"/>
                <g filter="url(#shadow)">
                    <rect x="143" y="48" width="194" height="245"
                          rx="22" fill="url(#paper)" stroke="#DBEAFE" stroke-width="3"/>
                    <rect x="207" y="37" width="66" height="27"
                          rx="10" fill="#6366F1"/>
                    <g fill="none" stroke="#3B82F6" stroke-width="6"
                       stroke-linecap="round" stroke-linejoin="round">
                        <path d="M169 112l10 10 19-22"/>
                        <path d="M169 171l10 10 19-22"/>
                    </g>
                    <g stroke="#CBD5E1" stroke-width="9" stroke-linecap="round">
                        <path d="M216 114h87"/>
                        <path d="M216 173h87"/>
                        <path d="M169 231h134"/>
                    </g>
                </g>
                <circle cx="353" cy="235" r="38" fill="url(#check)"/>
                <path d="M336 235l12 12 23-26"
                      fill="none" stroke="white" stroke-width="7"
                      stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            """,

            ["logo.svg"] = """
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 560 120">
                <defs>
                    <linearGradient id="logo" x1="0" y1="0" x2="1" y2="1">
                        <stop stop-color="#2563EB"/>
                        <stop offset="1" stop-color="#7C3AED"/>
                    </linearGradient>
                </defs>
                <rect x="8" y="12" width="96" height="96"
                      rx="26" fill="url(#logo)"/>
                <g fill="none" stroke="white" stroke-width="7"
                   stroke-linecap="round" stroke-linejoin="round">
                    <path d="M34 48L76 38L58 81Z"/>
                </g>
                <g fill="white">
                    <circle cx="34" cy="48" r="8"/>
                    <circle cx="76" cy="38" r="8"/>
                    <circle cx="58" cy="81" r="8"/>
                </g>
                <text x="125" y="78" font-family="Arial,sans-serif"
                      font-size="52" font-weight="700" fill="#172554">
                    Collab<tspan fill="#6366F1">Sphere</tspan>
                </text>
            </svg>
            """
        };

        foreach (var image in images)
        {
            string filePath = Path.Combine(outputPath, image.Key);

            File.WriteAllText(
                filePath,
                image.Value,
                System.Text.Encoding.UTF8
            );

            Console.WriteLine($"Created: {image.Key}");
        }

        Console.WriteLine("\nCreated successfully: 4 SVG files.");
    }
}

