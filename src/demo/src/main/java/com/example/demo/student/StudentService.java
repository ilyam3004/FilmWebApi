package com.example.demo.student;

import org.springframework.stereotype.Service;
import java.util.List;

@Service
public class StudentService {
    public List<Student> getStudents(){
        return List.of(
            new Student("Ilya", 19),
            new Student("Amigos", 20)
        );
    }
}
