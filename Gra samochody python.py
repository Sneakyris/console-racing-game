import random
import time


class Car:
    def __init__(self, driver, model):
        self.driver = driver
        self.model = model
        self.position_on_track = 0
        self.lap_times = []

    def total_time(self):
        return sum(self.lap_times)


class Track:
    def __init__(self, name, length):
        self.name = name
        self.length = length


class LapTimer:
    def generate_lap_time(self):
        return round(random.uniform(60, 80), 2)


class Race:
    def __init__(self, track, cars, laps):
        self.track = track
        self.cars = cars
        self.laps = laps
        self.lap_timer = LapTimer()

    def start_race(self):
        print(f"Wyścig na torze: {self.track.name}")
        print(f"Długość toru: {self.track.length} m")
        print(f"Liczba okrążeń: {self.laps}")
        print()

        for lap in range(1, self.laps + 1):
            print(f"--- Okrążenie {lap} ---")

            for car in self.cars:
                lap_time = self.lap_timer.generate_lap_time()
                car.lap_times.append(lap_time)
                car.position_on_track += self.track.length

                print(f"{car.driver} ({car.model}) - czas okrążenia: {lap_time} s")

            print()
            time.sleep(0.5)

        self.show_podium()

    def show_podium(self):
        print("=== PODIUM ===")

        results = sorted(self.cars, key=lambda car: car.total_time())

        for i, car in enumerate(results, start=1):
            print(f"{i}. {car.driver} - {car.model} | Łączny czas: {round(car.total_time(), 2)} s")


track = Track("Silverstone", 5800)

cars = [
    Car("Kierowca 1", "Ferrari"),
    Car("Kierowca 2", "Lamborghini"),
    Car("Kierowca 3", "Porsche"),
    Car("Kierowca 4", "BMW")
]

race = Race(track, cars, 3)
race.start_race()