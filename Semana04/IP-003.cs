using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

class Program {
    static void Main(){
        Academia academia = new Academia();

        Treinador treinador1 = new Treinador("José", new DateTime(1990, 12, 11), "12345678901", "CREF0204");
        Treinador treinador2 = new Treinador("Ana", new DateTime(1998, 3, 7), "10987654321", "CREF0103");

        Cliente cliente1 = new Cliente("André", new DateTime(1991, 3, 12), "11223344556", 1.89, 97);
        Cliente cliente2 = new Cliente("Raíssa", new DateTime(2000, 2, 2), "00998877665", 1.75, 70);

        academia.AddTreinador(treinador1);
        academia.AddTreinador(treinador2);

        academia.AddCliente(cliente1);
        academia.AddCliente(cliente2);

        //Relatórios
        Console.WriteLine("Treinadores entre 20 e 40 anos:");
        foreach (var treinador in academia.TreinadoresPorIdade(20, 40)) {
            Console.WriteLine(treinador.Nome);
        }

        Console.WriteLine("\nClientes entre 18 e 60 anos:");
        foreach (var cliente in academia.ClientesPorIdade(18, 60)) {
            Console.WriteLine(cliente.Nome);
        }

        Console.WriteLine("\nIMC de clientes maior que 25:");
        foreach (var cliente in academia.ClientesPorIMC(25)) {
            Console.WriteLine($"{cliente.Nome} - IMC: {cliente.CalcularIMC()}");
        }

        Console.WriteLine("\nClientes em ordem alfabética:");
        foreach (var cliente in academia.ClientesPorOrdemAlfabetica()) {
            Console.WriteLine(cliente.Nome);
        }

        Console.WriteLine("\nClientes em ordem descrente de idadae:");
        foreach (var cliente in academia.ClientesPorIdade(18, 60)) {
            Console.WriteLine($"{cliente.Nome} - Idade: {cliente.CalcularIdade()}");
        }

        Console.WriteLine("\nAniversariantes do mês de Novembro:");
        foreach (var pessoa in academia.AniversarianteDoMes(11)) {
            Console.WriteLine($"{pessoa.Nome} - Aniversário: {pessoa.DataNascimento.Day}/{pessoa.DataNascimento.Month}");
        }
    }

class Academia {
    private List<Treinador> treinadores;
    private List<Cliente> clientes;

    public Academia() {
        treinadores = new List<Treinador>();
        clientes = new List<Cliente>();
    }

    public void AddTreinador(Treinador treinador) {
        if (!treinadores.Any(t => t.CPF == treinador.CPF)) {
            treinadores.Add(treinador);
        }
        else {
            Console.WriteLine("Esse CPF pertence à um treinador já cadastrado.");
        }

        if
            (!treinadores.Any(t => t.CREF == treinador.CREF)) {
              treinadores.Add(treinador);  
            }
        else {
            Console.WriteLine("Esse CREF pertence à um treinador já cadastrado");
        }
    }

    public void AddCliente(Cliente cliente) {
        if (!clientes.Any(c => c.CPF == cliente.CPF)) {
            clientes.Add(cliente);
        }
        else {
            Console.WriteLine("Esse CPF pertence à um cliente já cadastrado");
        }
    }
    
    public IEnumerable<Treinador> TreinadoresPorIdade(int idadeMinima, int idadeMaxima) {
        return treinadores.Where(t => t.CalcularIdade() >= idadeMinima && t.CalcularIdade() <= idadeMaxima);
    }

    public IEnumerable<Cliente> ClientesPorIdade(int idadeMinima, int idadeMaxima) {
        return clientes.Where(c => c.CalcularIdade() >= idadeMinima && c.CalcularIdade() <= idadeMaxima);
    }

    public IEnumerable<Cliente> ClientesPorIMC(double valorMinimo) {
        return clientes.Where(c => c.CalcularIMC() > valorMinimo).OrderBy(c => c.CalcularIMC());
    }

    public IEnumerable<Cliente> ClientesPorOrdemAlfabetica() {
        return clientes.OrderBy(c => c.Nome);
    }

    public IEnumerable<Cliente> ClientesPorOrdemIdade() {
        return clientes.OrderByDescending(c => c.CalcularIdade());
    }

    public IEnumerable<Pessoa> AniversarianteDoMes(int mes) {
        return treinadores.Cast<Pessoa>().Concat(clientes.Cast<Pessoa>()).Where(p => p.DataNascimento.Month == mes);
    }
}
    
abstract class Pessoa {
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }

    public int CalcularIdade() {
        return DateTime.Now.Year - DataNascimento.Year - (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);
        }
}

class Treinador : Pessoa {
    public string CPF { get; set; }
    public string CREF { get; set; }

    public Treinador(string nome, DateTime dataNascimento, string cpf, string cref) {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        CREF = cref;
    }
}

class Cliente : Pessoa {
    public string CPF { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }

    public Cliente(string nome, DateTime dataNascimento, string cpf, double altura, double peso) {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        Altura = altura;
        Peso = peso;
    }
    public double CalcularIMC() {
        return Peso / (Altura * Altura);
    }
}
}
