#!/bin/bash
# Tamplate Cleanup Script for Linux/macOS
# This script stops Docker containers and removes migrations

echo "=== Tamplate Cleanup Script ==="
echo ""

# Stop and remove Docker containers
echo "[1/2] Stopping Docker containers..."
echo "Running: docker-compose down"

if ! docker-compose down; then
    echo "WARNING: Docker compose down failed or no containers were running"
fi

echo "Docker containers stopped"
echo ""

# Remove Migrations folder
echo "[2/2] Removing migrations folder..."

if [ -d "Tamplate.Domain/Migrations" ]; then
    echo "Deleting: Tamplate.Domain/Migrations"
    rm -rf "Tamplate.Domain/Migrations"
    echo "Migrations folder removed successfully"
else
    echo "Migrations folder does not exist, skipping..."
fi

echo ""
echo "=== Cleanup Complete! ==="
echo ""
echo "Docker containers have been stopped and migrations have been removed."
echo "You can now run setup.sh again to start fresh."
echo ""
